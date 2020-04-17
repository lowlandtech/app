import { ApiService } from '@lowlandtech/api';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { provideMockActions } from '@ngrx/effects/testing';
import { StoreModule } from '@ngrx/store';
import { hot } from '@nrwl/angular/testing';

import { CardService } from '../card.service';
import { CardEffects } from './card.effects';
import { ActionsService } from '@lowlandtech/shared';
import { NgrxFormsFacade } from '@lowlandtech/ngrx-forms';

describe('CardEffects', () => {
  let actions;
  let effects: CardEffects;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [StoreModule.forRoot({}), HttpClientTestingModule],
      providers: [
        CardEffects,
        provideMockActions(() => actions),
        CardService,
        ApiService,
        ActionsService,
        NgrxFormsFacade,
      ],
    });

    effects = TestBed.inject(CardEffects);
  });

  describe('someEffect', () => {
    it('should work', async () => {
      actions = hot('-a-|', { a: { type: 'LOAD_DATA' } });
      expect(true).toBeTruthy();
    });
  });
});
