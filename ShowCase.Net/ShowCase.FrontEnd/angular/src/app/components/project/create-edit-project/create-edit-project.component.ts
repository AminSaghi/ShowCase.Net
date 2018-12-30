import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { Project } from 'src/app/api-client/models';
import { ProjectService } from 'src/app/api-client';

@Component({
  selector: 'app-create-edit-project',
  templateUrl: './create-edit-project.component.html',
  styleUrls: ['./create-edit-project.component.css']
})
export class CreateEditProjectComponent implements OnInit {
  public editMode = false;
  public cardHeaderText = 'Create new Project';
  public commandButtonText = 'Create';
  public commandButtonClass = 'ui-button-success';
  public commandButtonIcon = 'pi pi-plus';

  projectForm;
  projects: Project[];
  projectModel: Project;
  public images: string[];

  constructor(private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private projectService: ProjectService,
    private location: Location) {

    this.projectModel = new Project();
    this.images = [];
    this.projectForm = formBuilder.group({
      id: new FormControl(this.projectModel.id),
      orderIndex: new FormControl(this.projectModel.orderIndex),
      title: new FormControl(this.projectModel.title, Validators.required),
      slug: new FormControl(this.projectModel.slug, Validators.required),
      description: new FormControl(this.projectModel.description),
      imageUrl: new FormControl(this.projectModel.imageUrl),
      published: new FormControl(this.projectModel.published)
    });
  }

  ngOnInit() {
    this.projectService.getProjects().subscribe(Projects => {
      this.projects = Projects;

      const id = this.activatedRoute.snapshot.params['id'];
      if (id) {
        this.projectService.getProject(id).subscribe(project => {console.log(project);
          this.projectForm.controls['id'].setValue(project.id);
          this.projectForm.controls['orderIndex'].setValue(project.orderIndex);
          this.projectForm.controls['title'].setValue(project.title);
          this.projectForm.controls['slug'].setValue(project.slug);
          this.projectForm.controls['description'].setValue(project.description);
          this.projectForm.controls['imageUrl'].setValue(project.imageUrl);
          this.projectForm.controls['published'].setValue(project.published);

          this.images.push(project.imageUrl);

          this.editMode = true;
          this.cardHeaderText = 'Edit Project';
          this.commandButtonText = 'Save';
          this.commandButtonClass = 'ui-button-warning';
          this.commandButtonIcon = 'pi pi-check';
        }, error => {
          this.goBack();
        });
      }
    });
  }

  createProject() {
    if (this.projectForm.valid) {
      const formValue = this.projectForm.value;
      console.log(formValue);
      this.projectService.createProject(formValue).subscribe();
    } else {
      Object.keys(this.projectForm.controls).forEach(field => {
        const control = this.projectForm.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    }
  }

  editProject() {
    if (this.projectForm.valid) {
      const formValue = this.projectForm.value;
      this.projectService.editProject(formValue).subscribe();
    } else {
      Object.keys(this.projectForm.controls).forEach(field => {
        const control = this.projectForm.get(field);
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
      that.projectForm.controls['imageUrl'].setValue(contents);
    };

    fileReader.readAsDataURL(imageFile);
  }

  clearImageUrl(event) {
    this.projectForm.controls['imageUrl'].setValue(null);
  }

  isInvalid(controlName) {
    return this.projectForm.controls[controlName].invalid && this.projectForm.controls[controlName].touched;
  }

  goBack() { this.location.back(); }
}
