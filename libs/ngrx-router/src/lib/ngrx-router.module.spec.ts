import { async, TestBed } from '@angular/core/testing';
import { NgrxRouterModule } from './ngrx-router.module';

describe('NgrxRouterModule', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [NgrxRouterModule]
    }).compileComponents();
  }));

  it('should create', () => {
    expect(NgrxRouterModule).toBeDefined();
  });
});
