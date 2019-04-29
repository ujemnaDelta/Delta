import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { modelGroupProvider } from '@angular/forms/src/directives/ng_model_group';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css', ]
})
export class LoginComponent implements OnInit {
  hide = true;
  mainMode = false;
  model: any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }
  login() {
    console.log(this.model);
    this.authService.login(this.model).subscribe(next => {
      this.mainMode = true;
    }, error => {
      console.log('Error');
    });
  }
  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

}
