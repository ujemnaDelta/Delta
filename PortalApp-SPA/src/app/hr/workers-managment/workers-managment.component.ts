import { Component, OnInit, ViewChild } from '@angular/core';
import { AddPersonDialogComponent } from 'src/app/admin/admin-panel/addPerson-dialog/addPerson-dialog.component';
import { MatDialogConfig, MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { User } from 'src/app/models/User';
import { AdminService } from 'src/app/services/admin.service';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-workers-managment',
  templateUrl: './workers-managment.component.html',
  styleUrls: ['./workers-managment.component.css']
})
export class WorkersManagmentComponent implements OnInit {
  displayedColumns: string[] = ['id', 'UserName', 'fullUserName', 'position', 'roles', 'team'];
  users: MatTableDataSource<User>;
  searchKey: string;
  constructor(private adminService: AdminService, private dialog: MatDialog, private alertify: AlertifyService) {
    dialog.afterAllClosed
    .subscribe(() => {
    // update a variable or call a function when the dialog closes
      this.getAll();
    }
  );
}

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
   this.getAll();
  }
  getAll() {
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
  dialogConfig.disableClose = false;
  dialogConfig.autoFocus = true;
  dialogConfig.width = '50%';
  this.dialog.open(AddPersonDialogComponent, dialogConfig);
}


}
