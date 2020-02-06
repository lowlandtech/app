import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'scx-aside-list',
  template: `
    <p>
      <button type="button" class="btn btn-primary btn-sm" (click)="addGroupItem()">
        Add Group Item
      </button>
    </p>

    <accordion>
      <accordion-group *ngFor="let group of groups" [heading]="group.title">
        {{ group?.content }}
      </accordion-group>
    </accordion>
  `,
  styles: []
})
export class AsideListComponent implements OnInit {
  public groups = [
    {
      title: 'Dynamic Group Header - 1',
      content: 'Dynamic Group Body - 1'
    },
    {
      title: 'Dynamic Group Header - 2',
      content: 'Dynamic Group Body - 2'
    }
  ];

  constructor() { }

  ngOnInit(): void {
  }

  addGroupItem(): void {
    this.groups.push({
      title: `Dynamic Group Header - ${this.groups.length + 1}`,
      content: `Dynamic Group Body - ${this.groups.length + 1}`
    });
  }
}
