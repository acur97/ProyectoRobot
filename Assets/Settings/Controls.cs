// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Controls.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class Controls : InputActionAssetReference
{
    public Controls()
    {
    }
    public Controls(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // Acciones
        m_Acciones = asset.GetActionMap("Acciones");
        m_Acciones_Disparo = m_Acciones.GetAction("Disparo");
        m_Acciones_Poder = m_Acciones.GetAction("Poder");
        m_Acciones_Pausa = m_Acciones.GetAction("Pausa");
        m_Acciones_Mover = m_Acciones.GetAction("Mover");
        // UI
        m_UI = asset.GetActionMap("UI");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_Acciones = null;
        m_Acciones_Disparo = null;
        m_Acciones_Poder = null;
        m_Acciones_Pausa = null;
        m_Acciones_Mover = null;
        m_UI = null;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // Acciones
    private InputActionMap m_Acciones;
    private InputAction m_Acciones_Disparo;
    private InputAction m_Acciones_Poder;
    private InputAction m_Acciones_Pausa;
    private InputAction m_Acciones_Mover;
    public struct AccionesActions
    {
        private Controls m_Wrapper;
        public AccionesActions(Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Disparo { get { return m_Wrapper.m_Acciones_Disparo; } }
        public InputAction @Poder { get { return m_Wrapper.m_Acciones_Poder; } }
        public InputAction @Pausa { get { return m_Wrapper.m_Acciones_Pausa; } }
        public InputAction @Mover { get { return m_Wrapper.m_Acciones_Mover; } }
        public InputActionMap Get() { return m_Wrapper.m_Acciones; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(AccionesActions set) { return set.Get(); }
    }
    public AccionesActions @Acciones
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new AccionesActions(this);
        }
    }
    // UI
    private InputActionMap m_UI;
    public struct UIActions
    {
        private Controls m_Wrapper;
        public UIActions(Controls wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
    }
    public UIActions @UI
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new UIActions(this);
        }
    }
    private int m_TecladoyMouseSchemeIndex = -1;
    public InputControlScheme TecladoyMouseScheme
    {
        get

        {
            if (m_TecladoyMouseSchemeIndex == -1) m_TecladoyMouseSchemeIndex = asset.GetControlSchemeIndex("Teclado y Mouse");
            return asset.controlSchemes[m_TecladoyMouseSchemeIndex];
        }
    }
    private int m_ControlSchemeIndex = -1;
    public InputControlScheme ControlScheme
    {
        get

        {
            if (m_ControlSchemeIndex == -1) m_ControlSchemeIndex = asset.GetControlSchemeIndex("Control");
            return asset.controlSchemes[m_ControlSchemeIndex];
        }
    }
}
