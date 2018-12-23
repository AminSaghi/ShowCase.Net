import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { Page } from 'src/app/api-client/models';
import { PageService } from 'src/app/api-client';

@Component({
  selector: 'app-create-edit-page',
  templateUrl: './create-edit-page.component.html',
  styleUrls: ['./create-edit-page.component.css']
})
export class CreateEditPageComponent implements OnInit {
  editMode = false;
  public cardHeaderText = 'Create new Page';
  public commandButtonText = 'Create';
  public commandButtonClass = 'ui-button-success';
  public commandButtonIcon = 'pi pi-plus';

  pageForm;
  pages: Page[];
  pageModel: Page;

  constructor(private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private pageService: PageService,
    private location: Location) {

    this.pageForm = formBuilder.group({
      id: new FormControl(this.pageModel.id),
      parentId: new FormControl(this.pageModel.parentId),
      orderIndex: new FormControl(this.pageModel.orderIndex),
      title: new FormControl(this.pageModel.title, Validators.required),
      slug: new FormControl(this.pageModel.slug),
      content: new FormControl(this.pageModel.content),

      parent: new FormControl(this.pageModel.parent),
    });
  }

  ngOnInit() {
    this.pageService.getPages().subscribe(pages => {
      this.pages = pages;

      const id = this.activatedRoute.snapshot.params['id'];
      if (id) {
        this.pageService.getPage(id).subscribe(page => {
          this.pageForm.controls['id'].setValue(page.id);
          this.pageForm.controls['parent'].setValue(
            page.parentId && page.parentId > 0 ?
              this.pages.find(p => p.id === page.parentId) :
              null
          );
          this.pageForm.controls['orderIndex'].setValue(page.orderIndex);
          this.pageForm.controls['title'].setValue(page.title);
          this.pageForm.controls['slug'].setValue(page.slug);
          this.pageForm.controls['content'].setValue(page.slug);

          this.editMode = true;
          this.cardHeaderText = 'Edit Page';
          this.commandButtonText = 'Save';
          this.commandButtonClass = 'ui-button-warning';
          this.commandButtonIcon = 'pi pi-check';
        }, error => {
          this.goBack();
        });
      }
    });
  }

  createPage() {
    if (this.pageForm.valid) {
      this.setParentIdFromParent();
      const formValue = this.pageForm.value;
      this.pageService.createPage(formValue).subscribe();
    } else {
      Object.keys(this.pageForm.controls).forEach(field => {
        const control = this.pageForm.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    }
  }

  editPage() {
    if (this.pageForm.valid) {
      this.setParentIdFromParent();
      const formValue = this.pageForm.value;
      this.pageService.editPage(formValue).subscribe();
    } else {
      Object.keys(this.pageForm.controls).forEach(field => {
        const control = this.pageForm.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    }
  }

  setParentIdFromParent() {
    this.pageForm.controls['parentId']
      .setValue(this.pageForm.controls['parent'].value ? this.pageForm.controls['parent'].value.id : null);
  }

  isInvalid(controlName) {
    return this.pageForm.controls[controlName].invalid && this.pageForm.controls[controlName].touched;
  }

  goBack() { this.location.back(); }
}
