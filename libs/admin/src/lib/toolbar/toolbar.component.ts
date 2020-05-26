import { Component } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd, Data } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'scx-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarsComponent {
  toolbar: Array<{
    url: string;
    label: Data;
  }>;
  constructor(private router: Router, private route: ActivatedRoute) {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
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
              url +=
                '/' + routeSnapshot.url.map(segment => segment.path).join('/');
              this.toolbar.push({
                label: route.snapshot.data,
                url: url
              });
              currentRoute = route;
            }
          });
        } while (currentRoute);
      });
  }
}
