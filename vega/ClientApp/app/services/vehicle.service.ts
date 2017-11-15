import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { SaveVehicle } from '../models/models';

@Injectable()
export class VehicleService {

    private readonly vehiclesEndpoint: string = '/api/vehicle';

    constructor(private http: Http) { }

    getMakes() {
        return this.http.get('/api/makes').map(r => r.json());
    }

    getFeatures() {
        return this.http.get('/api/features').map(r => r.json());
    }

    create(vehicle: SaveVehicle) {
        console.log(vehicle);
        return this.http.post(this.vehiclesEndpoint , vehicle).map(r => r.json());
    }

    get(id: number) {
        return this.http.get(this.vehiclesEndpoint + '/' + id).map(r => r.json());
    }

    getAll(filter: any){
        return this.http.get(this.vehiclesEndpoint + '?' + this.toQueryString(filter)).map(r => r.json());
    }

    toQueryString(obj: any){
        var parts = [];
        for(var property in obj){
            var value = obj[property];
            if(value != null && value != undefined){
                parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
            }
        }
        return parts.join('&');
    }

    update(vehicle: SaveVehicle){
        return this.http.put(this.vehiclesEndpoint + '/' + vehicle.id, vehicle).map(r => r.json());
    }

    delete(id: number){
        return this.http.delete(this.vehiclesEndpoint + '/' + id).map(r => r.json());
    }

}