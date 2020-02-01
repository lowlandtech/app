import { SharedModule } from '@spotacard/shared';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

import { ArticleListEffects } from './+state/card-list.effects';
import { ArticleListFacade } from './+state/card-list.facade';
import { articleListInitialState, articleListReducer } from './+state/card-list.reducer';
import { ArticleListItemComponent } from './card-list-item/card-list-item.component';
import { ArticleListComponent } from './card-list.component';
import { ArticleListService } from './card-list.service';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule,
    StoreModule.forFeature('articleList', articleListReducer, {
      initialState: articleListInitialState,
    }),
    EffectsModule.forFeature([ArticleListEffects]),
  ],
  declarations: [ArticleListComponent, ArticleListItemComponent],
  providers: [ArticleListService, ArticleListEffects, ArticleListFacade],
  exports: [ArticleListComponent],
})
export class ArticleListModule {}
