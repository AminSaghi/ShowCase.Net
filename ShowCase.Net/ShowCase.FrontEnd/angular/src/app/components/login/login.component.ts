import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { MessageService } from 'primeng/api';

import { AuthService } from 'src/app/api-client';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  public loginForm;

  loginModel = {
    username: '',
    password: ''
  };

  constructor(
    private auth: AuthService,
    private formBuilder: FormBuilder,
    private router: Router,
    private messageService: MessageService) {
    if (this.auth.isAuthenticated()) {
      this.auth.logout();
    }

    this.loginForm = formBuilder.group({
      username: new FormControl(this.loginModel.username, Validators.required),
      password: new FormControl(this.loginModel.password, Validators.required)
    });
  }

  performLogin() {
    if (this.loginForm.valid) {
      this.auth.login(this.loginForm.value).subscribe(response => {
        console.log(response);
        this.auth.setAuthToken(response['token']);
        this.router.navigate(['admin/dashboard']);
      }, error => {
        console.log(error);
        this.messageService.add({
          severity: 'error',
          summary: 'Login failed',
          detail: error.error
        });
      });
    }
  }
}
