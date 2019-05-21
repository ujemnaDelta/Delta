import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { User } from 'src/app/models/User';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-addPerson-dialog',
  templateUrl: './addPerson-dialog.component.html',
  styleUrls: ['./addPerson-dialog.component.css']
})
export class AddPersonDialogComponent implements OnInit {
  model: any = {};
  constructor( public dialogRef: MatDialogRef<User>) { }

  ngOnInit() {
  }

  register() {
    console.log(this.model);
  }


  onClose() {
    this.dialogRef.close();
    console.log('Canceled');
  }
}
