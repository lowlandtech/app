import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { AsideComponent } from './aside.component';
import { Component } from '@angular/core';

@Component({
  selector: 'scx-aside-test',
  template: `
    <scx-aside>
      <span title>I'm a test</span>
    </scx-aside>
  `
})
class AsideComponentTest {}

describe('AsideComponent', () => {
  let component: AsideComponent;
  let componentTest: AsideComponentTest;
  let fixture: ComponentFixture<AsideComponent>;
  let fixtureTest: ComponentFixture<AsideComponentTest>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AsideComponent, AsideComponentTest ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AsideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    fixtureTest = TestBed.createComponent(AsideComponentTest);
    componentTest = fixtureTest.componentInstance;
    fixtureTest.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should create component test', () => {
    expect(componentTest).toBeTruthy();
  });

  it('should have projected content', () => {
    expect(
      fixtureTest.debugElement.query(By.css('aside')).query(By.css('span'))
        .nativeElement.innerHTML
    ).toContain("I'm a test");
  });

});
