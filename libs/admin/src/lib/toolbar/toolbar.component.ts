import { Component } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd, Data } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'scx-toolbar',
  template: `
  <ol class="breadcrumb">
    <ng-template ngFor let-toolbar [ngForOf]="toolbar" let-last = last>
      <li class="breadcrumb-item"
          *ngIf="toolbar.label.title&&toolbar.url.substring(toolbar.url.length-1) == '/'||toolbar.label.title&&last"
          [ngClass]="{active: last}">
        <a *ngIf="!last" [routerLink]="toolbar.url">{{toolbar.label.title}}</a>
        <span *ngIf="last" [routerLink]="toolbar.url">{{toolbar.label.title}}</span>
      </li>
    </ng-template>
  </ol>`
})
export class ToolbarsComponent {
  toolbar: Array<{
    url: string;
    label: Data
  }>;
  constructor(
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.toolbar = [];
      let currentRoute = this.route.root,
      url = '';
      do {
        const childrenRoutes = currentRoute.children;
        currentRoute = null;
        // tslint:disable-next-line:no-shadowed-variable
        childrenRoutes.forEach(route => {
          if (route.outlet === 'primary') {
            const routeSnapshot = route.snapshot;
            url += '/' + routeSnapshot.url.map(segment => segment.path).join('/');
            this.toolbar.push({
              label: route.snapshot.data,
              url:   url
            });
            currentRoute = route;
          }
        });
      } while (currentRoute);
    });
  }
}
