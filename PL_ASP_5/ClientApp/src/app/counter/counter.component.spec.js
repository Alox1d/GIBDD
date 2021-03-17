import { async, TestBed } from '@angular/core/testing';
import { CounterComponent } from './counter.component';
describe('CounterComponent', function () {
    var fixture;
    beforeEach(async(function () {
        TestBed.configureTestingModule({
            declarations: [CounterComponent]
        })
            .compileComponents();
    }));
    beforeEach(function () {
        fixture = TestBed.createComponent(CounterComponent);
        fixture.detectChanges();
    });
    it('should display a title', async(function () {
        var titleText = fixture.nativeElement.querySelector('h1').textContent;
        expect(titleText).toEqual('Counter');
    }));
    it('should start with count 0, then increments by 1 when clicked', async(function () {
        var countElement = fixture.nativeElement.querySelector('strong');
        expect(countElement.textContent).toEqual('0');
        var incrementButton = fixture.nativeElement.querySelector('button');
        incrementButton.click();
        fixture.detectChanges();
        expect(countElement.textContent).toEqual('1');
    }));
});
//# sourceMappingURL=counter.component.spec.js.map