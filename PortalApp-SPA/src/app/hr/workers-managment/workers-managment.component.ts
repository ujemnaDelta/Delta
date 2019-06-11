import { Component, OnInit, ViewChild } from '@angular/core';
import { AddPersonDialogComponent } from 'src/app/admin/admin-panel/addPerson-dialog/addPerson-dialog.component';
import { MatDialogConfig, MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { User } from 'src/app/models/User';
import { AdminService } from 'src/app/services/admin.service';
import { AlertifyService } from 'src/app/services/alertify.service';
import { TeamLeaderPanel } from 'src/app/models/TeamLeaderPanel';
import { ShowopinionsDialogComponent } from 'src/app/leader/dialogs/showopinions-dialog/showopinions-dialog.component';

@Component({
  selector: 'app-workers-managment',
  templateUrl: './workers-managment.component.html',
  styleUrls: ['./workers-managment.component.css']
})
export class WorkersManagmentComponent implements OnInit {
  displayedColumns: string[] = ['id', 'UserName', 'fullUserName', 'position', 'roles', 'team'];
  users: MatTableDataSource<User>;
  searchKey: string;
  leader: any;
  constructor(private adminService: AdminService, private dialog: MatDialog, private alertify: AlertifyService) {
}

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
   this.getAll();
  }
  getLeaderId(team: string) {
    this.adminService.GetLeaderId(team).subscribe( user => {
      this.leader = user;
    }, error => {
      this.alertify.error(error);
    });
  }
  getAll() {
    this.adminService.getUsersWithRoles().subscribe((user: User[]) => {
      this.users = new MatTableDataSource(user);
      this.users.paginator = this.paginator;
    this.users.sort = this.sort;
    }, error => {
     this.alertify.error(error);
    });
  }
  applyFilter(filterValue: string) {
    this.users.filter = filterValue.trim().toLowerCase();

    if (this.users.paginator) {
      this.users.paginator.firstPage();
    }
  }
  onSearchClear() {
    this.searchKey = '';
    this.applyFilter(this.searchKey);
}

onCreateDialog() {
  const dialogConfig = new MatDialogConfig();
  dialogConfig.disableClose = false;
  dialogConfig.autoFocus = true;
  dialogConfig.width = '50%';
  this.dialog.open(AddPersonDialogComponent, dialogConfig);
}

OpenOpinions(user: any) {
  const dialogConfig2 = new MatDialogConfig();
  dialogConfig2.disableClose = false;
  dialogConfig2.autoFocus = true;
  dialogConfig2.width = '80%';
  dialogConfig2.height = '80%';
  dialogConfig2.data = { evaluatedId: user.id, leaderId: user.leaderId, name: user.fullUserName};
  this.dialog.open(ShowopinionsDialogComponent, dialogConfig2);

}

}
