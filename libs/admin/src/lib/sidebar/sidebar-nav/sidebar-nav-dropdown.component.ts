import { Component, Input } from '@angular/core';

@Component({
  selector: 'scx-sidebar-nav-dropdown',
  template: `
    <a class="nav-link nav-dropdown-toggle" scxNavDropdownToggle>
      <i *ngIf="isIcon()" class="{{ link.icon }}"></i>
      {{ link.name }}
      <span *ngIf="isBadge()" [ngClass]="'badge badge-' + link.badge.variant">{{ link.badge.text }}</span>
    </a>
    <ul class="nav-dropdown-items">
      <ng-template ngFor let-child [ngForOf]="link.children">
        <scx-sidebar-nav-item [item]='child'></scx-sidebar-nav-item>
      </ng-template>
    </ul>
  `
})
export class SidebarNavDropdownComponent {
  @Input() link: any;

  constructor() { }

  public isBadge() {
    return this.link.badge ? true : false
  }

  public isIcon() {
    return this.link.icon ? true : false
  }

}
