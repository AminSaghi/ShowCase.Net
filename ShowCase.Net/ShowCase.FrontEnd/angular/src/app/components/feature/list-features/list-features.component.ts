import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ConfirmationService } from 'primeng/api';

import { FeatureService } from 'src/app/api-client';

@Component({
  selector: 'app-list-features',
  templateUrl: './list-features.component.html',
  styleUrls: ['./list-features.component.css']
})
export class ListFeaturesComponent implements OnInit {

  constructor(
    private featureService: FeatureService,
    private router: Router,
    private confirmationService: ConfirmationService) { }

  features = [];
  pushedIds = [];
  public FeatureNodes;

  ngOnInit() {
    this.getFeatures();
  }

  getFeatures() {
    this.featureService.getFeatures().subscribe(response => {
      this.features = response;

      this.FeatureNodes = this.createTreeNodesOf(this.features);
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
      this.pushedIds = [];
      this.FeatureNodes = this.createTreeNodesOf(this.features);
    });
  }

  createTreeNodesOf(elements: any[]) {
    const that = this;
    const result = [];

    elements.forEach(function (node) {
      if (!that.pushedIds.includes(node.id)) {
        that.pushedIds.push(node.id);

        const treeNode = {
          'data': {
            'id': node.id,
            'orderIndex': node.orderIndex,
            'title': node.title,
            'slug': node.slug,
            'projectName': node.projectName,
            'updateDateTime': node.updateDateTime,
            'published': node.published
          },
          'children': (node.children && node.children.length > 0 ? that.createTreeNodesOf(node.children) : [])
        };

        result.push(treeNode);
      }
    });

    return result;
  }
}
