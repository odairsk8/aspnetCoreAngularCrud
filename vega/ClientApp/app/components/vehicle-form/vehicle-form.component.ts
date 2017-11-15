


import { Component, OnInit } from '@angular/core';
import { VehicleService } from './../../services/vehicle.service';
import { ToastyService } from 'ng2-toasty';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/Observable/forkJoin';
import * as _ from 'underscore';

import { SaveVehicle, Vehicle } from './../../models/models';

@Component({
    selector: 'app-vehicle-form',
    templateUrl: './vehicle-form.component.html',
    styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
    makes: any[];
    models: any[];
    features: any[];
    vehicle: SaveVehicle = {
        id: 0,
        isRegistered: false,
        makeId: 0,
        modelId: 0,
        features: [],
        contact: {
            name: '',
            email: '',
            phone: ''
        }
    };
    constructor(private vehicleService: VehicleService,
        private toastService: ToastyService,
        private router: Router,
        private activatedRouter: ActivatedRoute) {
        this.activatedRouter.params.subscribe(r => {
            if (r['id'])
                this.vehicle.id = +r['id'];
        });
    }


    ngOnInit() {

        let sources = [this.vehicleService.getMakes(), this.vehicleService.getFeatures()];
        if (this.vehicle.id)
            sources.push(this.vehicleService.get(this.vehicle.id));

        Observable.forkJoin(sources)
            .subscribe(
            success => {
                this.makes = success[0];
                this.features = success[1];
                if (this.vehicle.id) {
                    this.setVehicle(success[2]);
                    this.populateModels();
                }
            },
            error => {
                if (error.status == 404) {
                    this.router.navigate(['not-found']);
                }
            });
    }

    setVehicle(vehicle: Vehicle) {
        this.vehicle.id = vehicle.id || 0;
        this.vehicle.makeId = vehicle.make.id;
        this.vehicle.modelId = vehicle.model.id;
        this.vehicle.contact = vehicle.contact;
        this.vehicle.features = _.pluck(vehicle.features, 'id');
    }

    onMakeChange() {
        this.populateModels();
        delete this.vehicle.modelId;
    }

    private populateModels() {
        let selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
        this.models = selectedMake ? selectedMake.models : [];
    }

    onFeatureToogle(featureId: number, $event: any) {
        if ($event.target.checked) {
            this.vehicle.features.push(featureId);
        } else {
            let index = this.vehicle.features.indexOf(featureId);
            this.vehicle.features.splice(index, 1);
        }
    }

    submit() {
        let saveAction$ = this.vehicle.id ? this.vehicleService.update(this.vehicle) : this.vehicleService.create(this.vehicle);
        saveAction$.subscribe(
            success => {
                this.toastService.success({
                    title: 'Sucess',
                    msg: 'Saved with sucess',
                    theme: 'bootstrap',
                    showClose: true,
                    timeout: 5000
                });
            });
    }

}
