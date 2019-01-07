import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { MessageService } from 'primeng/api';

import { AuthService } from 'src/app/api-client';
import { ChangePasswordApiModel } from 'src/app/api-client/models/api-models';
import { Helpers } from 'src/app/shared/helpers';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {

  public changePasswordForm;
  changePasswordModel: ChangePasswordApiModel;

  constructor(private auth: AuthService,
    private formBuilder: FormBuilder,
    private messageService: MessageService) {

    this.changePasswordModel = new ChangePasswordApiModel();
    this.changePasswordForm = formBuilder.group({
      currentPassword: new FormControl(this.changePasswordModel.currentPassword, Validators.required),
      newPassword: new FormControl(this.changePasswordModel.newPassword, Validators.required),
      confirmNewPassword: new FormControl(this.changePasswordModel.confirmNewPassword, Validators.required)
    });
  }

  performChangePassword() {
    if (this.changePasswordForm.valid) {
      this.auth.changePassword(this.changePasswordForm.value).subscribe(response => {
        Helpers.addToast(this.messageService, 'success', 'Done', response);
      });
    }
  }

  isInvalid(controlName) {
    return this.changePasswordForm.controls[controlName].invalid && this.changePasswordForm.controls[controlName].touched;
  }
}
