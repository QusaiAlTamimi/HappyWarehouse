import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WarehouseService } from 'src/app/apis/warehouse.service';
import { AuthService } from 'src/app/core/services/authService';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {

  }

}
