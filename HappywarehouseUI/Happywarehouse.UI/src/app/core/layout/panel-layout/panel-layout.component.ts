import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/authService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-panel-layout',
  templateUrl: './panel-layout.component.html',
  styleUrls: ['./panel-layout.component.css']
})
export class PanelLayoutComponent implements OnInit {
  userName: string = localStorage.getItem('userName') ?? '';
  userEmail: string = localStorage.getItem('userEmail') ?? '';

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  navigate(link: string){
    this.router.navigate([link]);
  }

  logout(){
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
