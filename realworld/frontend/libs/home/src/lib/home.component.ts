import { Observable, Subject } from 'rxjs';
import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

import { Store } from '@ngrx/store';

import * as fromHome from './+state/home.reducer';
import * as fromAuth from '@spotacard/auth';
import { withLatestFrom, tap, takeUntil } from 'rxjs/operators';
import * as fromArticleList from '@spotacard/card-list';
import { ArticleListConfig } from '@spotacard/card-list';
import { articleListInitialState, ArticleListFacade } from '@spotacard/card-list';
import { AuthFacade } from '@spotacard/auth';
import { HomeFacade } from './+state/home.facade';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomeComponent implements OnInit, OnDestroy {
  listConfig$: Observable<ArticleListConfig>;
  tags$: Observable<string[]>;
  isAuthenticated: boolean;
  unsubscribe$: Subject<void> = new Subject();

  constructor(
    private facade: HomeFacade,
    private router: Router,
    private articleListFacade: ArticleListFacade,
    private authFacade: AuthFacade,
  ) {}

  ngOnInit() {
    this.authFacade.isLoggedIn$.pipe(takeUntil(this.unsubscribe$)).subscribe(isLoggedIn => {
      this.isAuthenticated = isLoggedIn;
      this.getArticles();
    });
    this.listConfig$ = this.articleListFacade.listConfig$;
    this.tags$ = this.facade.tags$;
  }

  setListTo(type: string = 'ALL') {
    this.articleListFacade.setListConfig(<ArticleListConfig>{
      ...articleListInitialState.listConfig,
      type,
    });
  }

  getArticles() {
    if (this.isAuthenticated) {
      this.setListTo('FEED');
    } else {
      this.setListTo('ALL');
    }
  }

  setListTag(tag: string) {
    this.articleListFacade.setListConfig(<ArticleListConfig>{
      ...articleListInitialState.listConfig,
      filters: {
        ...articleListInitialState.listConfig.filters,
        tag,
      },
    });
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
