import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ConfirmationService } from 'primeng/api';

import { PageService } from 'src/app/api-client';
import { Helpers } from 'src/app/shared/helpers';

@Component({
  selector: 'app-list-pages',
  templateUrl: './list-pages.component.html',
  styleUrls: ['./list-pages.component.css']
})
export class ListPagesComponent implements OnInit {

  constructor(
    private pageService: PageService,
    private router: Router,
    private confirmationService: ConfirmationService) { }

  pages = [];
  pushedIds = [];
  public pageNodes;

  ngOnInit() {
    this.getPages();
  }

  getPages() {
    this.pageService.getPages().subscribe(response => {
      this.pages = response;

      this.pageNodes = Helpers.createTreeNodesOf('', this.pages);
    });
  }

  confirmDelete(id: number) {
    this.confirmationService.confirm({
      message: 'Are you sure that you want to delete this page?',
      accept: () => {
        this.deletePage(id);
      }
    });
  }

  deletePage(pageId: number) {
    this.pageService.deletePage(pageId).subscribe(() => {
      this.pages = this.pages.filter(i => i.id !== pageId);
      this.pushedIds = [];
      this.pageNodes = Helpers.createTreeNodesOf('', this.pages);
    });
  }
}
