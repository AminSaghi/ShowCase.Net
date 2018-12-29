import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { forkJoin } from 'rxjs';

import { Feature, Project } from 'src/app/api-client/models';
import { FeatureService, ProjectService } from 'src/app/api-client';

@Component({
  selector: 'app-create-edit-feature',
  templateUrl: './create-edit-feature.component.html',
  styleUrls: ['./create-edit-feature.component.css']
})
export class CreateEditFeatureComponent implements OnInit {
  editMode = false;
  public cardHeaderText = 'Create new Feature';
  public commandButtonText = 'Create';
  public commandButtonClass = 'ui-button-success';
  public commandButtonIcon = 'pi pi-plus';

  projects: Project[];

  featureForm;
  features: Feature[];
  featureModel: Feature;

  constructor(private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private projectService: ProjectService,
    private featureService: FeatureService,
    private location: Location) {

    this.featureForm = formBuilder.group({
      id: new FormControl(this.featureModel.id),
      projectId: new FormControl(this.featureModel.projectId),
      parentId: new FormControl(this.featureModel.parentId),
      orderIndex: new FormControl(this.featureModel.orderIndex),
      title: new FormControl(this.featureModel.title, Validators.required),
      slug: new FormControl(this.featureModel.slug),
      content: new FormControl(this.featureModel.content),

      project: new FormControl(this.featureModel.project),
      parent: new FormControl(this.featureModel.parent),
    });
  }

  ngOnInit() {
    forkJoin(
      this.projectService.getProjects(),
      this.featureService.getFeatures()).subscribe(responses => {
        this.projects = responses[0];
        this.features = responses[1];

        const id = this.activatedRoute.snapshot.params['id'];
        if (id) {
          const feature = this.features.find(f => f.id === id);
          if (feature) {
            this.featureForm.controls['id'].setValue(feature.id);
            this.featureForm.controls['project'].setValue(
              this.projects.find(p => p.id === feature.projectId)
            );
            this.featureForm.controls['parent'].setValue(
              feature.parentId && feature.parentId > 0 ?
                this.features.find(p => p.id === feature.parentId) :
                null
            );
            this.featureForm.controls['orderIndex'].setValue(feature.orderIndex);
            this.featureForm.controls['title'].setValue(feature.title);
            this.featureForm.controls['slug'].setValue(feature.slug);
            this.featureForm.controls['content'].setValue(feature.slug);

            this.editMode = true;
            this.cardHeaderText = 'Edit Feature';
            this.commandButtonText = 'Save';
            this.commandButtonClass = 'ui-button-warning';
            this.commandButtonIcon = 'pi pi-check';
          } else {
            this.goBack();
          }
        }
      });
  }

  createFeature() {
    if (this.featureForm.valid) {
      this.setParentIdFromParent();
      const formValue = this.featureForm.value;
      this.featureService.createFeature(formValue).subscribe();
    } else {
      Object.keys(this.featureForm.controls).forEach(field => {
        const control = this.featureForm.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    }
  }

  editFeature() {
    if (this.featureForm.valid) {
      this.setProjectIdFromProject();
      this.setParentIdFromParent();
      const formValue = this.featureForm.value;
      this.featureService.editFeature(formValue).subscribe();
    } else {
      Object.keys(this.featureForm.controls).forEach(field => {
        const control = this.featureForm.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    }
  }

  setProjectIdFromProject() {
    this.featureForm.controls['projectId'].setValue(this.featureForm.controls['project'].value.id);
  }
  setParentIdFromParent() {
    this.featureForm.controls['parentId']
      .setValue(this.featureForm.controls['parent'].value ? this.featureForm.controls['parent'].value.id : 0);
  }

  isInvalid(controlName) {
    return this.featureForm.controls[controlName].invalid && this.featureForm.controls[controlName].touched;
  }

  goBack() { this.location.back(); }
}
