import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder, FormControl } from '@angular/forms';

import { SettingsService } from 'src/app/api-client';
import { Settings } from 'src/app/api-client/models';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  settingsForm;
  settingsModel: Settings;
  public images: string[];

  constructor(private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private settingsService: SettingsService,
    private location: Location) {
    this.settingsModel = new Settings();
    this.images = [];
    this.settingsForm = formBuilder.group({
      logoUrl: new FormControl(this.settingsModel.logoUrl),
      footerContent: new FormControl(this.settingsModel.footerContent)
    });
  }

  ngOnInit() {
    this.settingsService.getSettings().subscribe(settings => {
      this.settingsForm.controls['logoUrl'].setValue(settings.logoUrl);
      this.settingsForm.controls['footerContent'].setValue(settings.footerContent);

      this.images.push(settings.logoUrl);
    }, error => {
      this.goBack();
    });
  }

  editSettings() {
    if (this.settingsForm.valid) {
      const formValue = this.settingsForm.value;
      this.settingsService.editSettings(formValue).subscribe();
    } else {
      Object.keys(this.settingsForm.controls).forEach(field => {
        const control = this.settingsForm.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    }
  }

  getBase64Image(event) {
    const that = this;

    const imageFile = event.files[0];
    const fileReader = new FileReader();
    fileReader.onload = function (e: any) {
      const contents = e.target.result;
      that.settingsForm.controls['logoUrl'].setValue(contents);
    };

    fileReader.readAsDataURL(imageFile);
  }

  clearImageUrl(event) {
    this.settingsForm.controls['logoUrl'].setValue(null);
  }

  goBack() { this.location.back(); }
}
