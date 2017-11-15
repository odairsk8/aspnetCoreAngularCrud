import { Vehicle, KeyValuePair } from './../../../models/models';
import { VehicleService } from './../../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {

  private readonly PAGE_SIZE = 5;

  //allVehicles: Array<Vehicle>;
  queryResult: any = {};
  vehicles: Array<Vehicle> = [];
  makes: Array<KeyValuePair> = [];
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  totalItems = 0;
  columns = [
    { title: 'id' },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    { title: 'Contact', key: 'contactName', isSortable: true },
    {}
  ];

  constructor(private vehicleService: VehicleService) {

  }

  ngOnInit() {

    this.vehicleService.getMakes().subscribe(r => {
      this.makes = r;
    });
    this.populateVehicles();
  }

  populateVehicles() {
    this.vehicleService.getAll(this.query).subscribe(result => this.queryResult = result);
  }

  reset() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
    this.populateVehicles();
  }

  onFilterChange() {

    this.query.page = 1;
    this.populateVehicles();

    // filter on client
    // var vehicles = this.allVehicles;

    // if(this.filter.makeId)
    //   vehicles = vehicles.filter(v => v.make.id == this.filter.makeId);

    // if(this.filter.modelId)
    //   vehicles = vehicles.filter(v => v.make.id == this.filter.modelId);

    // this.vehicles = vehicles;
    
  }

  onPageChange(pageNumber: any ){
    this.query.page = pageNumber;
    this.populateVehicles();
  }

  sortBy(columnName: string) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateVehicles();
  }

  

}
