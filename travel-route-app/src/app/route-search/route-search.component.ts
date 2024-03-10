import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-route-search',
  templateUrl: './route-search.component.html',
  styleUrls: ['./route-search.component.css']
})
export class RouteSearchComponent implements OnInit {
  routes: any[] = [];

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.loadRoutes();
  }

  loadRoutes(): void {
    this.apiService.getRoutes().subscribe(
      (data) => {
        this.routes = data;
      },
      (error) => {
        console.error('Error loading routes:', error);
      }
    );
  }
}
