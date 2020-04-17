import { ApiService } from '@lowlandtech/api';
import { ActionsService } from '@lowlandtech/shared';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { provideMockActions } from '@ngrx/effects/testing';
import { StoreModule } from '@ngrx/store';
import { DataPersistence } from '@nrwl/angular';
import { hot } from '@nrwl/angular/testing';

import { CardListService } from '../card-list.service';
import { CardListEffects } from './card-list.effects';
import { CardListFacade } from './card-list.facade';

describe('CardListEffects', () => {
  let actions;
  let effects: CardListEffects;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        StoreModule.forRoot({}),
        HttpClientTestingModule],
      providers: [
        CardListEffects,
        DataPersistence,
        provideMockActions(() => actions),
        ActionsService,
        CardListService,
        ApiService,
        CardListFacade,
      ],
    });

    effects = TestBed.inject(CardListEffects);
  });

  describe('someEffect', () => {
    it('should work', async () => {
      actions = hot('-a-|', { a: { type: 'LOAD_DATA' } });
      expect(true).toBeTruthy();
    });
  });
});
