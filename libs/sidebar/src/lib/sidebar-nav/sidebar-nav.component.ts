import { Component } from '@angular/core';
import { navigation } from '../_nav';

@Component({
  selector: 'scx-sidebar-nav',
  template: `
    <nav class="sidebar-nav">
      <ul class="nav">
        <ng-template ngFor let-navitem [ngForOf]="navigation">
          <li *ngIf="isDivider(navitem)" class="nav-divider"></li>
          <ng-template [ngIf]="isTitle(navitem)">
            <scx-sidebar-nav-title [title]='navitem'></scx-sidebar-nav-title>
          </ng-template>
          <ng-template [ngIf]="!isDivider(navitem)&&!isTitle(navitem)">
            <scx-sidebar-nav-item [item]='navitem'></scx-sidebar-nav-item>
          </ng-template>
        </ng-template>
      </ul>
    </nav>`
})
export class SidebarNavComponent {

  public navigation = navigation;

  public isDivider(item) {
    return item.divider ? true : false
  }

  public isTitle(item) {
    return item.title ? true : false
  }

  constructor() { }
}
