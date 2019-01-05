import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { DomSanitizer } from '@angular/platform-browser';

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
    private pageService: PageService,
    public sanitizer: DomSanitizer) { }

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

  getPageContent(htmlString: string) {
    return this.sanitizer.bypassSecurityTrustHtml(htmlString);
  }

  goBack() { this.location.back(); }
}
