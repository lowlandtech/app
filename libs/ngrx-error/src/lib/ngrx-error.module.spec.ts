import { async, TestBed } from '@angular/core/testing';
import { NgrxErrorModule } from './ngrx-error.module';

describe('NgrxErrorModule', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [NgrxErrorModule]
    }).compileComponents();
  }));

  it('should create', () => {
    expect(NgrxErrorModule).toBeDefined();
  });
});
