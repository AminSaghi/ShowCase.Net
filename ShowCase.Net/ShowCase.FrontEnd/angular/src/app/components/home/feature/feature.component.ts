import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { FeatureService } from 'src/app/api-client';
import { Feature } from 'src/app/api-client/models';

@Component({
  selector: 'app-feature',
  templateUrl: './feature.component.html',
  styleUrls: ['./feature.component.css']
})
export class FeatureComponent implements OnInit {

  constructor(
    private activatedRoute: ActivatedRoute,
    private location: Location,
    private featureService: FeatureService) { }

  feature: Feature;

  ngOnInit() {
    const id = this.activatedRoute.snapshot.params['id'];
    if (id) {
      this.featureService.getFeature(id).subscribe(response => {
        this.feature = response;
      }, error => {
        this.goBack();
      });
    } else {
      this.goBack();
    }
  }

  goBack() { this.location.back(); }

}
