import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { PageService } from 'src/app/api-client';
import { Page } from 'src/app/api-client/models';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.css']
})
export class PageComponent implements OnInit {

  constructor(
    private activatedRoute: ActivatedRoute,
    private location: Location,
    private pageService: PageService) { }

    page: Page;

  ngOnInit() {
    const id = this.activatedRoute.snapshot.params['id'];
    if (id) {
      this.pageService.getPage(id).subscribe(response => {
        this.page = response;
      }, error => {
        this.goBack();
      });
    } else {
      this.goBack();
    }
  }

  goBack() { this.location.back(); }
}
