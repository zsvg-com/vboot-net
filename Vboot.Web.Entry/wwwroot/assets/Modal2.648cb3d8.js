import{B as u,a as c}from"./index.7c24f3ba.js";import{_ as d}from"./index.1f50be75.js";import{A as p,a0 as n,B as _,a1 as m,a6 as o,w as r,ae as i}from"./vendor.8e08a5be.js";import"./useWindowSizeFn.2016176c.js";const f=p({components:{BasicModal:u},setup(){const[e,{closeModal:t,setModalProps:s}]=c();return{register:e,closeModal:t,setModalProps:()=>{s({title:"Modal New Title"})}}}}),M=i(" \u4ECE\u5185\u90E8\u5173\u95ED\u5F39\u7A97 "),C=i(" \u4ECE\u5185\u90E8\u4FEE\u6539title ");function E(e,t,s,B,k,b){const a=n("a-button"),l=n("BasicModal");return _(),m(l,{onRegister:e.register,title:"Modal Title",helpMessage:["\u63D0\u793A1","\u63D0\u793A2"],okButtonProps:{disabled:!0}},{default:o(()=>[r(a,{type:"primary",onClick:e.closeModal,class:"mr-2"},{default:o(()=>[M]),_:1},8,["onClick"]),r(a,{type:"primary",onClick:e.setModalProps},{default:o(()=>[C]),_:1},8,["onClick"])]),_:1},8,["onRegister"])}var x=d(f,[["render",E]]);export{x as default};