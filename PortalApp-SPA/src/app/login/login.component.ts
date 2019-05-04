import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { modelGroupProvider } from '@angular/forms/src/directives/ng_model_group';
import { AlertifyService } from '../services/alertify.service';
import { Router } from '@angular/router';
import { AuthGuard } from '../guards/auth.guard';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css', ]
})
export class LoginComponent implements OnInit {
  hide = true;
  model: any = {};
  constructor(private router: Router, public authService: AuthService, private alertify: AlertifyService, public guard: AuthGuard) { }

  ngOnInit() {
  }
  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in successfully');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.router.navigate(['/main']);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
}

}
