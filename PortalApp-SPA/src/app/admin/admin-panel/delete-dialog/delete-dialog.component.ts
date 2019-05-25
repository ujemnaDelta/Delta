import { Component, OnInit, Inject } from '@angular/core';
import { User } from 'src/app/models/User';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-delete-dialog',
  templateUrl: './delete-dialog.component.html',
  styleUrls: ['./delete-dialog.component.css']
})
export class DeleteDialogComponent implements OnInit {
constructor(
  public dialogRef: MatDialogRef<DeleteDialogComponent>,
  @Inject(MAT_DIALOG_DATA) public user: User) {}

  ngOnInit() {
    console.log(this.user);
  }

}
