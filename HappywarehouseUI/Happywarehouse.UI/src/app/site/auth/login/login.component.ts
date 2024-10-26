import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { userService } from 'src/app/apis/user.service';
import { LoginRequest } from 'src/app/core/models/user';
import { AuthService } from 'src/app/core/services/authService';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userLogin: LoginRequest = new LoginRequest("", "");
  errorMessage: string | null = null;

  constructor(private authService: AuthService, private userServer: userService, private router: Router) {
    if (authService.isAuthenticated()) {
      this.router.navigate(['/dashboard']);
    }
  }

  ngOnInit(): void {}

  onSubmit(event: Event) {
    event.preventDefault(); // Prevent default form submission

    this.userServer.login(this.userLogin).subscribe({
      next: response => {
        if (response) {
          if (response.isAuthenticated) {
            this.saveAndRedirect(response.token, response.username, response.email);
          } else {
            this.errorMessage = response.message;
          }
        }
      },
      error: error => {
        if (typeof error.error === 'string')
          this.errorMessage = error.error;
        else
          this.errorMessage = 'Login failed. Please try again.';
      }
    });
  }

  saveAndRedirect(receivedToken: string, userName: string, userEmail: string) {
    this.authService.saveToken(receivedToken);
    localStorage.setItem('userEmail',userEmail);
    localStorage.setItem('userName',userEmail);
    this.router.navigate(['/dashboard']);
  }
}
