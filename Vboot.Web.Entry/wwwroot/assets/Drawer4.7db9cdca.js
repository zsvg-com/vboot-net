import{B as p,u as m}from"./index.b3a760e0.js";import{B as d}from"./BasicForm.4740433f.js";import{u as c}from"./useForm.6f5945bb.js";import{_ as l}from"./index.1f50be75.js";import{A as u,a0 as i,B as f,a1 as j,a6 as x,H as B,w as b,a4 as w}from"./vendor.8e08a5be.js";/* empty css               *//* empty css               *//* empty css               *//* empty css               *//* empty css                *//* empty css              *//* empty css               *//* empty css                *//* empty css                */import"./index.4765eeb8.js";/* empty css                */import"./index.7c24f3ba.js";import"./useWindowSizeFn.2016176c.js";/* empty css                *//* empty css                *//* empty css                *//* empty css                */import"./uuid.2b29000c.js";import"./download.1020b52a.js";import"./base64Conver.08b9f4ec.js";import"./index.b2563f35.js";const t=[{field:"field1",component:"Input",label:"\u5B57\u6BB51",colProps:{span:12},defaultValue:"111"},{field:"field2",component:"Input",label:"\u5B57\u6BB52",colProps:{span:12}}],_=u({components:{BasicDrawer:p,BasicForm:d},setup(){const[e,{setFieldsValue:s}]=c({labelWidth:120,schemas:t,showActionButtonGroup:!1,actionColOptions:{span:24}}),[r]=m(o=>{s({field2:o.data,field1:o.info})});return{register:r,schemas:t,registerForm:e}}});function F(e,s,r,o,g,h){const n=i("BasicForm"),a=i("BasicDrawer");return f(),j(a,w(e.$attrs,{onRegister:e.register,title:"Drawer Title",width:"50%"}),{default:x(()=>[B("div",null,[b(n,{onRegister:e.registerForm},null,8,["onRegister"])])]),_:1},16,["onRegister"])}var U=l(_,[["render",F]]);export{U as default};
