import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { TeamManagment } from 'src/app/models/TeamManagment';
import { AdminService } from 'src/app/services/admin.service';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-deleteteam-dialog',
  templateUrl: './deleteteam-dialog.component.html',
  styleUrls: ['./deleteteam-dialog.component.css']
})
export class DeleteteamDialogComponent implements OnInit {

  constructor( public dialogRef: MatDialogRef<DeleteteamDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public team: TeamManagment,
    private adminService: AdminService,
    private alertify: AlertifyService) {}

    ngOnInit() {
    }
    deleteUser() {
      this.adminService.deleteTeam(this.team.id).subscribe(() => {
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
