import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { MessageService } from 'primeng/api';

import { User } from 'src/app/api-client/models';
import { AuthService } from 'src/app/api-client';
import { Helpers } from 'src/app/shared/helpers';

@Component({
  selector: 'app-create-edit-user',
  templateUrl: './create-edit-user.component.html',
  styleUrls: ['./create-edit-user.component.css']
})
export class CreateEditUserComponent implements OnInit {

  public editMode = false;
  public cardHeaderText = 'Create new User';
  public commandButtonText = 'Create';
  public commandButtonClass = 'ui-button-success';
  public commandButtonIcon = 'pi pi-plus';

  userForm;
  users: User[];
  userModel: User;
  public images: string[];

  constructor(private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private messageService: MessageService,
    private location: Location) {

    this.userModel = new User();
    this.images = [];
    this.userForm = formBuilder.group({
      id: new FormControl(this.userModel.id),
      userName: new FormControl(this.userModel.userName, Validators.required),
      email: new FormControl(this.userModel.email, Validators.required),
      password: new FormControl(this.userModel.password, Validators.required),
      confirmPassword: new FormControl(this.userModel.confirmPassword, Validators.required)
    });
  }

  ngOnInit() {
    this.authService.getUsers().subscribe(users => {
      this.users = users;

      const id = this.activatedRoute.snapshot.params['id'];
      if (id) {
        this.authService.getUser(id).subscribe(user => {
          this.userForm.controls['id'].setValue(user.id);
          this.userForm.controls['userName'].setValue(user.userName);
          this.userForm.controls['email'].setValue(user.email);

          this.userForm.removeControl('password');
          this.userForm.removeControl('confirmPassword');

          this.editMode = true;
          this.cardHeaderText = 'Edit User';
          this.commandButtonText = 'Save';
          this.commandButtonClass = 'ui-button-warning';
          this.commandButtonIcon = 'pi pi-check';
        }, error => {
          this.goBack();
        });
      }
    });
  }

  createUser() {
    if (this.userForm.valid) {
      Helpers.addCreatingToast(this.messageService, 'User');

      const formValue = this.userForm.value;
      this.authService.createUser(formValue).subscribe(respones => {
        Helpers.addToast(this.messageService, 'success', 'Done', respones);
      });
    } else {
      Object.keys(this.userForm.controls).forEach(field => {
        const control = this.userForm.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    }
  }

  editUser() {
    if (this.userForm.valid) {
      Helpers.addUpdatingToast(this.messageService, 'User');

      const formValue = this.userForm.value;
      this.authService.editUser(formValue).subscribe(response => {
        Helpers.addToast(this.messageService, 'success', 'Done', response);
      });
    } else {
      Object.keys(this.userForm.controls).forEach(field => {
        const control = this.userForm.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    }
  }

  isInvalid(controlName) {
    return this.userForm.controls[controlName].invalid && this.userForm.controls[controlName].touched;
  }

  goBack() { this.location.back(); }
}
