import { Observable, Subject } from 'rxjs';
import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

import { Store } from '@ngrx/store';

import * as fromHome from './+state/home.reducer';
import * as fromAuth from '@spotacard/auth';
import { withLatestFrom, tap, takeUntil } from 'rxjs/operators';
import * as fromCardList from '@spotacard/card-list';
import { CardListConfig } from '@spotacard/card-list';
import { cardListInitialState, CardListFacade } from '@spotacard/card-list';
import { AuthFacade } from '@spotacard/auth';
import { HomeFacade } from './+state/home.facade';

@Component({
  selector: 'scx-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomeComponent implements OnInit, OnDestroy {
  listConfig$: Observable<CardListConfig>;
  tags$: Observable<string[]>;
  isAuthenticated: boolean;
  unsubscribe$: Subject<void> = new Subject();

  constructor(
    private facade: HomeFacade,
    private router: Router,
    private cardListFacade: CardListFacade,
    private authFacade: AuthFacade,
  ) {}

  ngOnInit() {
    this.authFacade.isLoggedIn$.pipe(takeUntil(this.unsubscribe$)).subscribe(isLoggedIn => {
      this.isAuthenticated = isLoggedIn;
      this.getCards();
    });
    this.listConfig$ = this.cardListFacade.listConfig$;
    this.tags$ = this.facade.tags$;
  }

  setListTo(type: string = 'ALL') {
    this.cardListFacade.setListConfig(<CardListConfig>{
      ...cardListInitialState.listConfig,
      type,
    });
  }

  getCards() {
    if (this.isAuthenticated) {
      this.setListTo('FEED');
    } else {
      this.setListTo('ALL');
    }
  }

  setListTag(tag: string) {
    this.cardListFacade.setListConfig(<CardListConfig>{
      ...cardListInitialState.listConfig,
      filters: {
        ...cardListInitialState.listConfig.filters,
        tag,
      },
    });
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
