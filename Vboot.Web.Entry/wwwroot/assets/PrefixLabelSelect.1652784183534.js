var c=Object.defineProperty,m=Object.defineProperties;var f=Object.getOwnPropertyDescriptors;var o=Object.getOwnPropertySymbols;var n=Object.prototype.hasOwnProperty,x=Object.prototype.propertyIsEnumerable;var s=(e,l,t)=>l in e?c(e,l,{enumerable:!0,configurable:!0,writable:!0,value:t}):e[l]=t,p=(e,l)=>{for(var t in l||(l={}))n.call(l,t)&&s(e,t,l[t]);if(o)for(var t of o(l))x.call(l,t)&&s(e,t,l[t]);return e},d=(e,l)=>m(e,f(l));import{an as u}from"./index.1652784183534.js";import{a as v,B as V,Z as i,P as b}from"./vue.1652784183534.js";const T=v({props:d(p({},u.props),{prefixTitle:{type:String,default:()=>""}}),emits:["update:modelValue"],setup(e,{emit:l,slots:t}){const r=V({get:()=>e.value,set:a=>l("update:modelValue",a)});return()=>i("div",{class:"prefix-label-select-container"},[e.prefixTitle&&i("div",{class:"prefix-title "},[e.prefixTitle]),i(u,b({class:"prefix-label-select",modelValue:r.value,"onUpdate:modelValue":a=>r.value=a},e),t)])}});export{T as P};