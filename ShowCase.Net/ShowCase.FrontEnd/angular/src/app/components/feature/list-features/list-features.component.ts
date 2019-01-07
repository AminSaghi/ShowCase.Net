import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ConfirmationService, MessageService } from 'primeng/api';

import { FeatureService } from 'src/app/api-client';
import { Helpers } from 'src/app/shared/helpers';

@Component({
  selector: 'app-list-features',
  templateUrl: './list-features.component.html',
  styleUrls: ['./list-features.component.css']
})
export class ListFeaturesComponent implements OnInit {

  constructor(
    private featureService: FeatureService,
    private router: Router,
    private messageService: MessageService,
    private confirmationService: ConfirmationService) { }

  features = [];
  pushedIds = [];
  public featureNodes;

  ngOnInit() {
    this.getFeatures();
  }

  getFeatures() {
    Helpers.addToast(this.messageService, 'info', 'Loading data', 'Please wait ...');

    this.featureService.getFeatures().subscribe(response => {
      this.features = response;
      this.featureNodes = Helpers.createTreeNodesOf('feature', this.features);
    });
  }

  confirmDelete(id: number) {
    this.confirmationService.confirm({
      message: 'Are you sure that you want to delete this Feature?',
      accept: () => {
        this.deleteFeature(id);
      }
    });
  }

  deleteFeature(FeatureId: number) {
    this.featureService.deleteFeature(FeatureId).subscribe(() => {
      this.features = this.features.filter(i => i.id !== FeatureId);

      this.featureNodes = Helpers.createTreeNodesOf('feature', this.features);
    });
  }
}
