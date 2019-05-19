import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/models/User';
import { AdminService } from 'src/app/services/admin.service';
import {MatPaginator, MatSort, MatTableDataSource} from '@angular/material';


@Component({
  selector: 'app-user-managment',
  templateUrl: './user-managment.component.html',
  styleUrls: ['./user-managment.component.css']
})
export class UserManagmentComponent implements OnInit {
  displayedColumns: string[] = ['id', 'userName', 'roles', 'Action'];
  users: MatTableDataSource<User>;
  constructor(private adminService: AdminService) {
  }

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
    this.adminService.getUsersWithRoles().subscribe((user: User[]) => {
      this.users = new MatTableDataSource(user);
      this.users.paginator = this.paginator;
    this.users.sort = this.sort;

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

}


