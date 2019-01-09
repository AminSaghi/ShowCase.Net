import { Component, OnInit } from '@angular/core';

import { ConfirmationService } from 'primeng/api';

import { UserService } from 'src/app/api-client';

@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html',
  styleUrls: ['./list-users.component.css']
})
export class ListUsersComponent implements OnInit {

  constructor(private userService: UserService,
    private confirmationService: ConfirmationService) { }

  public users = [];

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.userService.getUsers().subscribe(response => {
      this.users = response;
    });
  }

  confirmDelete(id: string) {
    this.confirmationService.confirm({
      message: 'Are you sure that you want to delete this User?',
      accept: () => {
        this.deleteUser(id);
      }
    });
  }

  deleteUser(userId: string) {
    this.userService.deleteUser(userId).subscribe(() => {
      this.users = this.users.filter(i => i.id !== userId);
    });
  }

}
