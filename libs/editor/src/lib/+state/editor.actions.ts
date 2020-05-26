import { CardData } from '@lowlandtech/api/src/lib/types';
import { createAction, props } from '@ngrx/store';

export const publishCard = createAction('[editor] PUBLISH_ARTICLE');
export const initializeCard = createAction('[editor] INITIALIZE_ARTICLE');
export const loadCard = createAction('[editor] LOAD_ARTICLE', props<{ id: string }>());
export const loadCardSuccess = createAction('[editor] LOAD_ARTICLE_SUCCESS', props<{ card: CardData }>());
export const loadCardFail = createAction('[editor] LOAD_ARTICLE_FAIL', props<{ error: Error }>());
