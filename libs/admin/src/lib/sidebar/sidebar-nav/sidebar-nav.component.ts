import { Component, HostBinding } from '@angular/core';
import { navigation } from '../../navigation';

@Component({
  selector: 'scx-sidebar-nav',
  template: `
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
    </ul>`
})
export class SidebarNavComponent {
  @HostBinding('class.sidebar-nav') class1 = true

  constructor() { }
  public navigation = navigation;

  public isDivider(item: { divider: any; }) {
    return item.divider ? true : false
  }

  public isTitle(item: { title: any; }) {
    return item.title ? true : false
  }
}
