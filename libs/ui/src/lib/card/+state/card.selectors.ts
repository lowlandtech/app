import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromCard from './card.reducer';

export const selectCardState = createFeatureSelector<fromCard.CardState>(
  fromCard.cardFeatureKey
);
