import { Component, OnInit, Inject } from '@angular/core';
import { User } from 'src/app/models/User';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AdminService } from 'src/app/services/admin.service';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-delete-dialog',
  templateUrl: './delete-dialog.component.html',
  styleUrls: ['./delete-dialog.component.css']
})
export class DeleteDialogComponent implements OnInit {
constructor(
  public dialogRef: MatDialogRef<DeleteDialogComponent>,
  @Inject(MAT_DIALOG_DATA) public user: User,
  private adminService: AdminService,
  private alertify: AlertifyService) {}

  ngOnInit() {

  }
  deleteUser() {
    this.adminService.deleteUser(this.user.id).subscribe(() => {
      this.alertify.success('Pomyślnie usunięto użytkownika');
      this.onClose();
    }, error => {
      this.alertify.error(error);
    });
  }
  onClose() {
    this.dialogRef.close();
  }
}
