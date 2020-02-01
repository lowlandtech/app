import { NgrxFormsModule } from '@spotacard/ngrx-forms';
import { SharedModule } from '@spotacard/shared';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

import { ArticleEffects } from './+state/card.effects';
import { ArticleFacade } from './+state/card.facade';
import { articleInitialState, articleReducer } from './+state/card.reducer';
import { AddCommentComponent } from './add-comment/add-comment.component';
import { ArticleCommentComponent } from './card-comment/card-comment.component';
import { ArticleGuardService } from './card-guard.service';
import { ArticleMetaComponent } from './card-meta/card-meta.component';
import { ArticleComponent } from './card.component';
import { ArticleService } from './card.service';
import { MarkdownPipe } from './markdown.pipe';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: ArticleComponent,
        canActivate: [ArticleGuardService],
      },
    ]),
    StoreModule.forFeature('card', articleReducer, {
      initialState: articleInitialState,
    }),
    EffectsModule.forFeature([ArticleEffects]),
    NgrxFormsModule,
    SharedModule,
  ],
  providers: [ArticleEffects, ArticleService, ArticleGuardService, ArticleFacade],
  declarations: [ArticleComponent, ArticleMetaComponent, ArticleCommentComponent, MarkdownPipe, AddCommentComponent],
})
export class ArticleModule {}
