import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/models/User';
import { AdminService } from 'src/app/services/admin.service';
import {MatPaginator, MatSort, MatTableDataSource, MatDialog, MatDialogConfig} from '@angular/material';
import { AddPersonDialogComponent } from '../admin-panel/addPerson-dialog/addPerson-dialog.component';
import { DeleteDialogComponent } from '../admin-panel/delete-dialog/delete-dialog.component';
import { equal } from 'assert';


@Component({
  selector: 'app-user-managment',
  templateUrl: './user-managment.component.html',
  styleUrls: ['./user-managment.component.css']
})
export class UserManagmentComponent implements OnInit {
  displayedColumns: string[] = ['id', 'UserName', 'fullUserName', 'roles', 'team', 'Action'];
  users: MatTableDataSource<User>;
  searchKey: string;
  constructor(private adminService: AdminService, private dialog: MatDialog) {
  }

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
    this.adminService.getUsersWithRoles().subscribe((user: User[]) => {
      this.users = new MatTableDataSource(user);
      this.users.paginator = this.paginator;
    this.users.sort = this.sort;
    console.log(user);
    }, error => {
      console.log(error);
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
  dialogConfig.disableClose = true;
  dialogConfig.autoFocus = true;
  dialogConfig.width = '50%';
  this.dialog.open(AddPersonDialogComponent, dialogConfig);
}


onCreateDeleteDialog(user: User) {
  const dialogConfig2 = new MatDialogConfig();
  dialogConfig2.disableClose = true;
  dialogConfig2.autoFocus = true;
  dialogConfig2.width = '50%';
  user = this.users.data.find(p => p.id === user.id);
  dialogConfig2.data = { id: user.id, userName : user.userName, fullUserName: user.fullUserName,
    team: user.team, roles: user.roles};
  this.dialog.open(DeleteDialogComponent, dialogConfig2);
}
}


