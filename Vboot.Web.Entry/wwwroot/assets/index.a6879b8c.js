var N=Object.defineProperty;var f=Object.getOwnPropertySymbols;var P=Object.prototype.hasOwnProperty,g=Object.prototype.propertyIsEnumerable;var j=(e,t,n)=>t in e?N(e,t,{enumerable:!0,configurable:!0,writable:!0,value:n}):e[t]=n,S=(e,t)=>{for(var n in t||(t={}))P.call(t,n)&&j(e,n,t[n]);if(f)for(var n of f(t))g.call(t,n)&&j(e,n,t[n]);return e};import{A,cQ as r,r as E,P as w,W as k,a0 as s,B as p,a1 as u,a6 as _,H as x,w as i,F as a,G as d,ad as v}from"./vendor.8e08a5be.js";import R from"./Step1.54e32a12.js";import W from"./Step2.10f0d694.js";import y from"./Step3.1784b330.js";import{P as D}from"./index.4a6d20cf.js";/* empty css                *//* empty css                */import{_ as V}from"./index.1f50be75.js";import"./BasicForm.4740433f.js";/* empty css               *//* empty css               *//* empty css               *//* empty css                *//* empty css              *//* empty css               *//* empty css                *//* empty css                */import"./index.4765eeb8.js";/* empty css                */import"./index.7c24f3ba.js";import"./useWindowSizeFn.2016176c.js";/* empty css                *//* empty css                *//* empty css                */import"./uuid.2b29000c.js";import"./download.1020b52a.js";import"./base64Conver.08b9f4ec.js";import"./index.b2563f35.js";import"./useForm.6f5945bb.js";import"./data.2505cb7f.js";/* empty css                *//* empty css               *//* empty css               *//* empty css                */import"./useContentViewHeight.78015564.js";const $=A({name:"FormStepPage",components:{Step1:R,Step2:W,Step3:y,PageWrapper:D,[r.name]:r,[r.Step.name]:r.Step},setup(){const e=E(0),t=w({initSetp2:!1,initSetp3:!1});function n(o){e.value++,t.initSetp2=!0,console.log(o)}function m(){e.value--}function c(o){e.value++,t.initSetp3=!0,console.log(o)}function l(){e.value=0,t.initSetp2=!1,t.initSetp3=!1}return S({current:e,handleStep1Next:n,handleStep2Next:c,handleRedo:l,handleStepPrev:m},k(t))}}),H={class:"step-form-form"},z={class:"mt-5"};function G(e,t,n,m,c,l){const o=s("a-step"),F=s("a-steps"),h=s("Step1"),B=s("Step2"),C=s("Step3"),b=s("PageWrapper");return p(),u(b,{title:"\u5206\u6B65\u8868\u5355",contentBackground:"",content:" \u5C06\u4E00\u4E2A\u5197\u957F\u6216\u7528\u6237\u4E0D\u719F\u6089\u7684\u8868\u5355\u4EFB\u52A1\u5206\u6210\u591A\u4E2A\u6B65\u9AA4\uFF0C\u6307\u5BFC\u7528\u6237\u5B8C\u6210\u3002",contentClass:"p-4"},{default:_(()=>[x("div",H,[i(F,{current:e.current},{default:_(()=>[i(o,{title:"\u586B\u5199\u8F6C\u8D26\u4FE1\u606F"}),i(o,{title:"\u786E\u8BA4\u8F6C\u8D26\u4FE1\u606F"}),i(o,{title:"\u5B8C\u6210"})]),_:1},8,["current"])]),x("div",z,[a(i(h,{onNext:e.handleStep1Next},null,8,["onNext"]),[[d,e.current===0]]),e.initSetp2?a((p(),u(B,{key:0,onPrev:e.handleStepPrev,onNext:e.handleStep2Next},null,8,["onPrev","onNext"])),[[d,e.current===1]]):v("",!0),e.initSetp3?a((p(),u(C,{key:1,onRedo:e.handleRedo},null,8,["onRedo"])),[[d,e.current===2]]):v("",!0)])]),_:1})}var Ce=V($,[["render",G],["__scopeId","data-v-71d43ad8"]]);export{Ce as default};
