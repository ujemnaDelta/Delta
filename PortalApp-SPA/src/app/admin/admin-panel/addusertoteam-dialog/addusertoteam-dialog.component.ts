import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/services/admin.service';
import { AuthService } from 'src/app/services/auth.service';
import { AlertifyService } from 'src/app/services/alertify.service';
import { MatDialogRef } from '@angular/material';
import { FormBuilder, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-addusertoteam-dialog',
  templateUrl: './addusertoteam-dialog.component.html',
  styleUrls: ['./addusertoteam-dialog.component.css']
})
export class AddusertoteamDialogComponent implements OnInit {
  users: any = [];
  teams: any = [];
  usersLogins: any = [];
  isDisabled = true;
  myForm = this.fb.group({
    FullUserName: ['', [Validators.required]],
    UserLogin: [{value : '', disabled: this.isDisabled}, [Validators.required]],
    UserTeam: ['', [Validators.required]],
});
  constructor( public dialogRef: MatDialogRef<AddusertoteamDialogComponent>, private adminService: AdminService,
    private alertify: AlertifyService, private authService: AuthService, private fb: FormBuilder) { }

  ngOnInit() {
    this.getTeams();
    this.getUsers();
  }

//   register(from: FormControl) {
//     this.authService.register(form).subscribe(() => {
//       this.alertify.success('registration successful');
//       this.onClose();
//     }, error => {
//       this.alertify.error(error);
//     });
// }
GetUserLogin(surname: string) {
  this.adminService.getUsersByName(surname).subscribe(response => {
   this.usersLogins = response;
   this.isDisabled = false;
   this.myForm.get('UserLogin').enable();
  }, error => {
    this.alertify.error('Failed load teams');
  });
}
GetLoginName(form: FormControl, login: string) {
  form.get('UserLogin').setValue(login);
}
Add(form: FormControl) {
  this.adminService.addTeamMates(form.value).subscribe(res => {
  }, error => {
    this.alertify.error(error);
 });
}
  getTeams() {
    this.adminService.getTeam().subscribe(response => {
      this.teams = response;
    }, error => {
      this.alertify.error('Failed load teams');
    });
  }

  getUsers() {
    this.adminService.getUsersWithRoles().subscribe((response: []) => {
      this.users = response;
    }, error => {
      this.alertify.error('Failed load roles');
    });
  }
  onClose() {
    this.dialogRef.close();
  }
}
