import{h as d,B as c}from"./BasicForm.4740433f.js";import{_ as l,h as j}from"./index.1f50be75.js";import{u as f}from"./useForm.6f5945bb.js";import{P as x}from"./index.4a6d20cf.js";import{A as g,cl as r,a0 as o,B as F,a1 as B,a6 as _,w as s}from"./vendor.8e08a5be.js";/* empty css                */import{u as t}from"./upload.8ae821a4.js";/* empty css               *//* empty css               *//* empty css               *//* empty css                *//* empty css              *//* empty css               *//* empty css                *//* empty css                */import"./index.4765eeb8.js";/* empty css                */import"./index.7c24f3ba.js";import"./useWindowSizeFn.2016176c.js";/* empty css                *//* empty css                *//* empty css                */import"./uuid.2b29000c.js";import"./download.1020b52a.js";import"./base64Conver.08b9f4ec.js";import"./index.b2563f35.js";/* empty css               *//* empty css                */import"./useContentViewHeight.78015564.js";const b=[{field:"field1",component:"Upload",label:"\u5B57\u6BB51",colProps:{span:8},rules:[{required:!0,message:"\u8BF7\u9009\u62E9\u4E0A\u4F20\u6587\u4EF6"}],componentProps:{api:t}}],h=g({components:{BasicUpload:d,BasicForm:c,PageWrapper:x,[r.name]:r},setup(){const{createMessage:e}=j(),[i]=f({labelWidth:120,schemas:b,actionColOptions:{span:16}});return{handleChange:a=>{e.info(`\u5DF2\u4E0A\u4F20\u6587\u4EF6${JSON.stringify(a)}`)},uploadApi:t,register:i}}});function C(e,i,a,A,E,P){const n=o("a-alert"),p=o("BasicUpload"),m=o("BasicForm"),u=o("PageWrapper");return F(),B(u,{title:"\u4E0A\u4F20\u7EC4\u4EF6\u793A\u4F8B"},{default:_(()=>[s(n,{message:"\u57FA\u7840\u793A\u4F8B"}),s(p,{maxSize:20,maxNumber:10,onChange:e.handleChange,api:e.uploadApi,class:"my-5",accept:["image/*"]},null,8,["onChange","api"]),s(n,{message:"\u5D4C\u5165\u8868\u5355,\u52A0\u5165\u8868\u5355\u6821\u9A8C"}),s(m,{onRegister:e.register,class:"my-5"},null,8,["onRegister"])]),_:1})}var se=l(h,[["render",C]]);export{se as default};