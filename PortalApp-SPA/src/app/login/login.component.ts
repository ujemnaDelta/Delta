import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { modelGroupProvider } from '@angular/forms/src/directives/ng_model_group';
import { AlertifyService } from '../services/alertify.service';
import { Router } from '@angular/router';
import { AuthGuard } from '../guards/auth.guard';
import {FormControl, Validators, FormBuilder} from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css', ]
})
export class LoginComponent implements OnInit {
  hide = true;
  model: any = {};
  myForm = this.fb.group({
    userName: ['', [Validators.required]],
    userPassword: ['',  [Validators.required,
      Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}')]]

});

  constructor(private router: Router, public authService: AuthService, private alertify: AlertifyService, public guard: AuthGuard,
    private fb: FormBuilder) { }

  ngOnInit() {
  }
  login(form: FormControl) {
    this.authService.login(form.value).subscribe(next => {
      this.alertify.success('Zalogowano pomyślnie');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.router.navigate(['/home']);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('logged out');
    this.router.navigate(['/login']);
}

}
