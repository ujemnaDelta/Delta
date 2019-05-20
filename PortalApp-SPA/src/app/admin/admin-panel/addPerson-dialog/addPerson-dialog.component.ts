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

  constructor( public dialogRef: MatDialogRef<User>) { }

  ngOnInit() {
  }

  onClose() {
    this.dialogRef.close();
}
}
