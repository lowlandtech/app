import { ArticleData } from '@spotacard/api';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { withLatestFrom } from 'rxjs/operators';

import { ArticleListFacade } from './+state/card-list.facade';
import { ArticleListConfig } from './+state/card-list.reducer';

@Component({
  selector: 'app-card-list',
  templateUrl: './card-list.component.html',
  styleUrls: ['./card-list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ArticleListComponent implements OnInit {
  totalPages$: Observable<number[]>;
  articles$: Observable<ArticleData[]>;
  listConfig$: Observable<ArticleListConfig>;
  isLoading$: Observable<boolean>;

  constructor(private facade: ArticleListFacade) {}

  ngOnInit() {
    this.totalPages$ = this.facade.totalPages$;
    this.articles$ = this.facade.articles$;
    this.listConfig$ = this.facade.listConfig$;
    this.isLoading$ = this.facade.isLoading$;
  }

  favorite(slug: string) {
    this.facade.favorite(slug);
  }

  unFavorite(slug: string) {
    this.facade.unFavorite(slug);
  }

  navigateToArticle(slug: string) {
    this.facade.navigateToArticle(slug);
  }

  setPage(page: number) {
    this.facade.setPage(page);
  }
}
